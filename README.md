# FluentvalidationBR.Extensions#

Built-In validation package for Fluentvalidation

### How to install with Nuget ?
```
dotnet add package FluentvalidationBR.Extensions
```

### What is this repository for? ###

This package aims to extend some built-in validations that we already have in fluent validations.

For now we have:

 - CnpjValidator
 - CpfValidator
 - IntegerValidator
 - UriValidator
 - CellPhoneValidator
 - PhoneValidator
 - CepValidator
 - IpValidator

### How do I use it? ###

```.cs
class SampleValidator : AbstractValidator<Sample>
{
      public SampleValidator()
      {
          RuleFor(x => x.Celular).CellPhone();
          RuleFor(x => x.Number).Integer();
          RuleFor(x => x.Site).Uri();
          RuleFor(x => x.Cpf).Cpf();
          RuleFor(x => x.Cnpj).Cnpj();
      }
}

class Sample
{
     public string Site { get; set; }
     public string Number { get; set; }
     public string Cpf { get; set; }
     public string Cnpj { get; set; }
     public string Celular { get; set; }
}
 ```
 
