using System;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;
using Flunt.Validations;

namespace FluentValidationBR.Extensions
{
    public class CnpjValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => nameof(CnpjValidator<T>);

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um CNPJ válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value == null)
                return true;

            try
            {
                var result = CNPJ.IsValid(Regex.Replace(value, @"[./-]", "").ToUpper());

                if (!result)
                {
                    context.MessageFormatter.AppendArgument(nameof(CnpjValidator<T>), value);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }

    public class CNPJ
    {
        private const int TamanhoCNPJSemDV = 12;
        private static readonly Regex RegexCNPJSemDV = new Regex(@"^([A-Z\d]){12}$", RegexOptions.Compiled);
        private static readonly Regex RegexCNPJ = new Regex(@"^([A-Z\d]){12}(\d){2}$", RegexOptions.Compiled);
        private static readonly Regex RegexCaracteresNaoPermitidos = new Regex(@"[^A-Z\d./-]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private const int ValorBase = '0';
        private static readonly int[] PesosDV = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private const string CnpjZerado = "00000000000000";

        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            if (!RegexCaracteresNaoPermitidos.IsMatch(cnpj))
            {
                if (RegexCNPJ.IsMatch(cnpj) && cnpj != CnpjZerado)
                {
                    string dvInformado = cnpj.Substring(TamanhoCNPJSemDV);
                    string dvCalculado = CalculaDV(cnpj.Substring(0, TamanhoCNPJSemDV));

                    return dvInformado == dvCalculado;
                }
            }
            return false;
        }

        public static string CalculaDV(string cnpj)
        {
            if (!RegexCaracteresNaoPermitidos.IsMatch(cnpj))
            {
                if (RegexCNPJSemDV.IsMatch(cnpj) && cnpj != CnpjZerado.Substring(0, TamanhoCNPJSemDV))
                {
                    int somatorioDV1 = 0;
                    int somatorioDV2 = 0;

                    for (int i = 0; i < TamanhoCNPJSemDV; i++)
                    {
                        int asciiDigito = cnpj[i] - ValorBase;
                        somatorioDV1 += asciiDigito * PesosDV[i + 1];
                        somatorioDV2 += asciiDigito * PesosDV[i];
                    }

                    int dv1 = (somatorioDV1 % 11 < 2) ? 0 : 11 - (somatorioDV1 % 11);

                    // Adiciona o peso do primeiro DV calculado ao somatório do segundo
                    somatorioDV2 += dv1 * PesosDV[TamanhoCNPJSemDV];

                    int dv2 = (somatorioDV2 % 11 < 2) ? 0 : 11 - (somatorioDV2 % 11);

                    return $"{dv1}{dv2}";
                }
            }

            throw new ArgumentException("Não é possível calcular o DV pois o CNPJ fornecido é inválido");
        }
    }
}
