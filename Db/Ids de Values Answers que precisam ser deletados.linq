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
</Query>

void Main()
{
	var f = (from valueAnswer in VALUEANSWERs
			 join photo in PHOTOs on valueAnswer.IDPHOTO equals photo.IDPHOTO
			 select new
			 {
				 photo.NAME,
				 photo.ELECTED,
				 valueAnswer.HASCHOOSEN,
				 valueAnswer.IDPHOTO,
				 valueAnswer.IDVALUEANSWER,
				 valueAnswer.ANSWER.SURVEY.IDSURVEY,
				 valueAnswer.ANSWER.SURVEY.INITIALFILLDATE,
				 valueAnswer.ANSWER.SURVEY.FINALFILLDATE,
				 valueAnswer.ANSWER.IDANSWER,
			 }).ToList().GroupBy(d => d.IDSURVEY).Select(d =>
			 {
				 var registro = d.First(x => d.Key == x.IDSURVEY);
				 var resultado = new
				 {
					 IdentificadorQuestionario = d.Key,
					 DataInicioPreenchimento = registro.INITIALFILLDATE.AddHours(-3),
					 DataFinalPreenchimento = registro.FINALFILLDATE?.AddHours(-3),
					 Questoes = d.GroupBy(x => x.IDANSWER).Select(n => new
					 {
						 IdentificadorQuestao = n.Key,
						 Opcoes = n.Select(g => new
						 {
							 g.IDVALUEANSWER,
							 Candidato = g.NAME,
							 FoiEscolhido = Convert.ToBoolean(g.HASCHOOSEN) ? "Sim" : "Não",
							 Eleito = Convert.ToBoolean(g.ELECTED) ? "Sim" : "Não"
						 })
					 })
				 };

				 return resultado;
			 });

	//	 f.Count(d => d.Questoes.Where(e => e.Opcoes.Select(h => h.Eleito).Distinct().Count() == 2).Count() > 0).Dump();
	var he = f.Select(d => new { d.IdentificadorQuestionario, Invalidos = d.Questoes.Where(q => q.Opcoes.Select(e => e.Eleito).Distinct().Count() == 1) });
	//	.OrderByDescending(h => h.Invalidos).Dump();

	he.SelectMany(m => m.Invalidos).SelectMany(e => e.Opcoes).Select(k => k.IDVALUEANSWER).Select(i => "'"+i.ToString()+"'").Aggregate((a, p) => a + "," + p).Dump();
}


// Define other methods and classes here