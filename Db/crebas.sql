/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     20/05/2019 20:30:42                          */
/*==============================================================*/


drop table if exists FOTO;

drop table if exists PESQUISA;

drop table if exists PESSOA;

drop table if exists QUESTAO;

drop table if exists RESPOSTA;

drop table if exists VALORESRESPOSTA;

/*==============================================================*/
/* Table: FOTO                                                  */
/*==============================================================*/
create table FOTO
(
   IDFOTO               int not null auto_increment,
   IDGENERO             int,
   IDETNIA              int,
   HASHFOTO             varchar(50) not null,
   ELEITO               tinyint not null,
   primary key (IDFOTO)
);

/*==============================================================*/
/* Table: PESQUISA                                              */
/*==============================================================*/
create table PESQUISA
(
   IDPESQUISA           int not null auto_increment,
   IDPESSOA             int not null,
   HORAINICIOPREENCHIMENTO datetime,
   HORAFIMPREENCHIMENTO datetime,
   primary key (IDPESQUISA)
);

alter table PESQUISA comment 'Representa uma pesquisa';

/*==============================================================*/
/* Table: PESSOA                                                */
/*==============================================================*/
create table PESSOA
(
   IDPESSOA             int not null auto_increment,
   EMAILPESSOA          varchar(100),
   primary key (IDPESSOA)
);

alter table PESSOA comment 'Representa uma pessoa que respondeu a pesquisa';

/*==============================================================*/
/* Table: QUESTAO                                               */
/*==============================================================*/
create table QUESTAO
(
   IDQUESTAO            int not null auto_increment,
   DESCRICAOQUESTAO     varchar(500),
   primary key (IDQUESTAO)
);

/*==============================================================*/
/* Table: RESPOSTA                                              */
/*==============================================================*/
create table RESPOSTA
(
   IDRESPOSTA           int not null auto_increment,
   IDPESQUISA           int not null,
   IDQUESTAO            int not null,
   primary key (IDRESPOSTA)
);

alter table RESPOSTA comment 'Representa uma quest�o respondida';

/*==============================================================*/
/* Table: VALORESRESPOSTA                                       */
/*==============================================================*/
create table VALORESRESPOSTA
(
   IDVALORRESPOSTA      int not null auto_increment,
   IDRESPOSTA           int not null,
   IDFOTO               int not null,
   FOISELECIONADA       tinyint not null,
   TEMPOSELECAO         timestamp not null,
   primary key (IDVALORRESPOSTA)
);

alter table PESQUISA add constraint FK_RELATIONSHIP_1 foreign key (IDPESSOA)
      references PESSOA (IDPESSOA) on delete restrict on update restrict;

alter table RESPOSTA add constraint FK_RELATIONSHIP_2 foreign key (IDPESQUISA)
      references PESQUISA (IDPESQUISA) on delete restrict on update restrict;

alter table RESPOSTA add constraint FK_RELATIONSHIP_3 foreign key (IDQUESTAO)
      references QUESTAO (IDQUESTAO) on delete restrict on update restrict;

alter table VALORESRESPOSTA add constraint FK_RELATIONSHIP_4 foreign key (IDRESPOSTA)
      references RESPOSTA (IDRESPOSTA) on delete restrict on update restrict;

alter table VALORESRESPOSTA add constraint FK_RELATIONSHIP_5 foreign key (IDFOTO)
      references FOTO (IDFOTO) on delete restrict on update restrict;

