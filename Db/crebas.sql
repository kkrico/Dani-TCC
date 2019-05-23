/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     22/05/2019 21:53:37                          */
/*==============================================================*/


drop table if exists ANSWER;

drop table if exists PERSON;

drop table if exists PHOTO;

drop table if exists QUESTION;

drop table if exists SURVEY;

drop table if exists VALUEANSWER;

/*==============================================================*/
/* Table: ANSWER                                                */
/*==============================================================*/
create table ANSWER
(
   IDANSWER             int not null auto_increment,
   IDSURVEY             int not null,
   IDQUESTION           int not null,
   primary key (IDANSWER)
);

alter table ANSWER comment 'Representa uma questão respondida';

/*==============================================================*/
/* Table: PERSON                                                */
/*==============================================================*/
create table PERSON
(
   IDPERSON             int not null auto_increment,
   IDAGEGROUP           int,
   IDGENDER             int,
   IDETHNICITY          int,
   IDSEXUALITY          int,
   IDFAMILYINCOME       int,
   primary key (IDPERSON)
);

alter table PERSON comment 'Representa uma pessoa que respondeu a pesquisa';

/*==============================================================*/
/* Table: PHOTO                                                 */
/*==============================================================*/
create table PHOTO
(
   IDPHOTO              int not null auto_increment,
   IDGENDER             int,
   IDETHNICITY          int,
   PHOTOHASH            varchar(50) not null,
   ELECTED              tinyint not null,
   primary key (IDPHOTO)
);

/*==============================================================*/
/* Table: QUESTION                                              */
/*==============================================================*/
create table QUESTION
(
   IDQUESTION           int not null auto_increment,
   QUESTIONDESCRIPTION  varchar(500),
   primary key (IDQUESTION)
);

/*==============================================================*/
/* Table: SURVEY                                                */
/*==============================================================*/
create table SURVEY
(
   IDSURVEY             int not null auto_increment,
   IDPERSON             int,
   INITIALFILLDATE      datetime not null,
   FINALFILLDATE        datetime,
   primary key (IDSURVEY)
);

alter table SURVEY comment 'Representa uma pesquisa';

/*==============================================================*/
/* Table: VALUEANSWER                                           */
/*==============================================================*/
create table VALUEANSWER
(
   IDVALUEANSWER        int not null auto_increment,
   IDANSWER             int not null,
   IDPHOTO              int not null,
   HASCHOOSEN           tinyint not null,
   SELECTIONTIME        timestamp not null,
   primary key (IDVALUEANSWER)
);

alter table ANSWER add constraint FK_RELATIONSHIP_2 foreign key (IDSURVEY)
      references SURVEY (IDSURVEY) on delete restrict on update restrict;

alter table ANSWER add constraint FK_RELATIONSHIP_3 foreign key (IDQUESTION)
      references QUESTION (IDQUESTION) on delete restrict on update restrict;

alter table SURVEY add constraint FK_RELATIONSHIP_1 foreign key (IDPERSON)
      references PERSON (IDPERSON) on delete restrict on update restrict;

alter table VALUEANSWER add constraint FK_RELATIONSHIP_4 foreign key (IDANSWER)
      references ANSWER (IDANSWER) on delete restrict on update restrict;

alter table VALUEANSWER add constraint FK_RELATIONSHIP_5 foreign key (IDPHOTO)
      references PHOTO (IDPHOTO) on delete restrict on update restrict;

