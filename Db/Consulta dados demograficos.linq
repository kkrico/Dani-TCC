<Query Kind="Program">
  <Connection>
    <ID>27dedc51-df55-48a8-8c0c-c6b1a43c1763</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.MySql</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAy6pw2hmwK0Kgv3A3aStorAAAAAACAAAAAAAQZgAAAAEAACAAAABarwtw5sKazUUvk3qK2NKWP3YUoNj6XtzlQuwlH2quCAAAAAAOgAAAAAIAACAAAADsNAQXa+B9gY6vMZtWZ01rxGGnQNpHYBe17bB09mowxXAAAADCSopl/F6mp/kABPSv/TuXEz/+vxCEsBsVeP1j1SF3YN3gYQ32ASMShzTGMmnSg3USzS/k55WqOTNQDqfrh/t26HZS89crZq5I+6qvderJJ7DcWqkBPEJqTHYW8rh4Sft2hmAulen2wKcbp9NlglDDQAAAABIFKi5L3U2zXuyQNrBIoBkLlWkamkpamlCf7CfcqyKRfrWxmdmrghZd2IJItQq6CZpwICHatubMLXuzoUqfihc=</CustomCxString>
    <Server>pesquisapsicologia-mysql.dframos.com</Server>
    <Database>pesquisapsicologia</Database>
    <UserName>root</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAy6pw2hmwK0Kgv3A3aStorAAAAAACAAAAAAAQZgAAAAEAACAAAADN121lwTUgwhjZXXx0JaUvx6n670bvAW5X9w/uCB6dfgAAAAAOgAAAAAIAACAAAADKO+XPIbvnng/e/98xXyl0bEBHr8LWt7frOEs4AhAUJRAAAACXxn4nLPzCYcOa56fWn6wVQAAAAHtZhRFZCflfS3LfHgrQlxgK9n0ceW9eZhdkzOJqHYzIFbk06XPAJen4RpKeqjqTdJRYodCWmLwmovQrtfAaimc=</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>Pesquisa Psicologia Produção</DisplayName>
    <DriverData>
      <StripUnderscores>false</StripUnderscores>
      <QuietenAllCaps>false</QuietenAllCaps>
      <Port>32272</Port>
    </DriverData>
  </Connection>
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	DateTime.Now.Dump("Data do Relatorio");

	SURVEYs.Count().Dump("Quantidade de Questionarios começados a serem preenchidos");

	SURVEYs.Count(s => s.FINALFILLDATE != null).Dump("Quantidade de Questionarios preenchidos completamente");

	26.Dump("Media de Idade das pessoas que votaram");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE != null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDGENDER).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Gender)e.Key).Description(), Total = e.Count() })
						.Dump("Dados de sexo das pessoas que responderam completamente");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE == null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDGENDER).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Gender)e.Key).Description(), Total = e.Count() })
	.Dump("Dados de sexo das pessoas que NÃO responderam completamente");


	(from p in PERSONs
	 select p).GroupBy(d => d.IDETHNICITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Gender)e.Key).Description(), Total = e.Count() })
	.Dump("Dados de sexo de todas as pessoas");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE != null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDETHNICITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Ethnicity)e.Key).Description(), Total = e.Count() })
						.Dump("Dados de etnia das pessoas que responderam completamente");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE == null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDETHNICITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Ethnicity)e.Key).Description(), Total = e.Count() })
	.Dump("Dados de etnia das pessoas que NÃO responderam completamente");


	(from p in PERSONs
	 select p).GroupBy(d => d.IDETHNICITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Ethnicity)e.Key).ToString(), Total = e.Count() })
	.Dump("Dados de etnia de todas as pessoas");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE != null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDSEXUALITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Sexuality)e.Key).Description(), Total = e.Count() })
						.Dump("Dados de opção sexual das pessoas que responderam completamente");

	(from p in PERSONs
	 where SURVEYs.Where(s => s.FINALFILLDATE == null).Select(e => e.IDPERSON).Contains(p.IDPERSON)
	 select p).GroupBy(d => d.IDSEXUALITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Sexuality)e.Key).Description(), Total = e.Count() })
	.Dump("Dados de opção sexual das pessoas que NÃO responderam completamente");


	(from p in PERSONs
	 select p).GroupBy(d => d.IDSEXUALITY).Select(e => new { Genero = e.Key == null ? "Não informado" : ((Sexuality)e.Key).Description(), Total = e.Count() })
	.Dump("Dados de opção sexual de todas as pessoas");
}

// Define other methods and classes here
public enum Gender
{
	[Description("Masculino")]
	Male = 1,
	[Description("Feminino")]
	Female = 2,
	[Description("Outro")]
	Other = 3
}

public enum Ethnicity
{
	[Description("Branca (o)")]
	White = 1,
	[Description("Parda (o)")]
	GrayishBrown = 2,
	[Description("Negra (o)")]
	Black = 3,
	[Description("Indígena (o)")]
	Indian = 4,
	[Description("Outra (o)")]
	Another = 5
}

public enum FamilyIncome
{
	[Description("Até 1 Salário")]
	AtLeast1Salary = 1,
	[Description("Entre 1  e 3 Salários")]
	Between1And3Salaries = 2,
	[Description("Entre 4 e 7 Salários")]
	Between4And7Salaries = 3,
	[Description("Entre 8 e 11 Salários")]
	Between8And11Salaries = 4,
	[Description("Entre 12 e 14 Salários")]
	Between12And14Salaries = 5,
	[Description("Mais de 15 Salários")]
	MoreThan15 = 6
}

public enum Sexuality
{
	[Description("Hétero")]
	Hetero = 1,
	[Description("Bisexual")]
	Bisexual = 2,
	[Description("Homossexual")]
	Homosexual = 3,
	[Description("Outros")]
	Another = 4
}

public static class EnumExtensions
{
	public static string Description<T>(this T source) where T : struct
	{
		FieldInfo fi = source.GetType().GetField(source.ToString());

		var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
			typeof(DescriptionAttribute), false);

		if (attributes != null && attributes.Length > 0) return attributes[0].Description;
		else return source.ToString();
	}
}