-- Limpa base

USE DB_PESQUISA_TCC;

DELETE FROM VALUEANSWER
WHERE IDVALUEANSWER > -1;

DELETE FROM PHOTO
WHERE IDPHOTO > -1;

DELETE FROM ANSWER
WHERE IDANSWER > -1;

DELETE FROM SURVEY
WHERE IDSURVEY > -1;

DELETE FROM PERSON
WHERE IDPERSON > -1;
