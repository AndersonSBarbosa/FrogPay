CREATE DATABASE IF NOT EXISTS DBFrogPay;

Use DBFrogPay;
---- Table: public.Endereco

--DROP TABLE IF EXISTS public."Endereco";

CREATE TABLE IF NOT EXISTS public.Endereco
(
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "Uf" character varying(2) COLLATE pg_catalog."default",
    "Cidade" character varying(500) COLLATE pg_catalog."default",
    "Bairro" character varying(500) COLLATE pg_catalog."default",
    "Logradouro" character varying(500) COLLATE pg_catalog."default",
    "Numero" character varying(20) COLLATE pg_catalog."default",
    "Complemento" character varying(500) COLLATE pg_catalog."default",
    "PessoaId" bigint NOT NULL,
    CONSTRAINT tb_endereco_pkey PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Endereco"
    OWNER to postgres;	

	-----------------------------------------------------------

-- Table: public.Loja

--DROP TABLE IF EXISTS public."Loja";

CREATE TABLE IF NOT EXISTS public."Loja"
(
    "RazaoSocial" character varying(500) COLLATE pg_catalog."default",
    "Cnpj" character varying(50) COLLATE pg_catalog."default",
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "PessoaId" bigint NOT NULL,
    "NomeFantasia" character varying(500) COLLATE pg_catalog."default",
    CONSTRAINT tb_loja_pkey PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Loja"
    OWNER to postgres;
	
	
	
-------------------------------------------------------------------

-- Table: public.Pessoa

-- DROP TABLE IF EXISTS public."Pessoa";

CREATE TABLE IF NOT EXISTS public."Pessoa"
(
    "Nome" character varying(50) COLLATE pg_catalog."default",
    "Cpf" character varying(50) COLLATE pg_catalog."default",
    "DataNascimento" date,
    "Ativo" boolean,
    "DataCriacao" timestamp with time zone,
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    CONSTRAINT tb_pessoa_pkey PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Pessoa"
    OWNER to postgres;

------------------------------------------------


-- Table: public.DadosBancario

--DROP TABLE IF EXISTS public."DadosBancario";

CREATE TABLE IF NOT EXISTS public."DadosBancario"
(
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),      
    "CodigoBanco" character varying COLLATE pg_catalog."default",
    "Agencia" character varying COLLATE pg_catalog."default",
    "Conta" character varying COLLATE pg_catalog."default",
    "DigitoConta" character varying COLLATE pg_catalog."default",
    "PessoaId" bigint NOT NULL,
    CONSTRAINT tb_dados_bancarios_pkey PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."DadosBancario"
    OWNER to postgres;
	
--	-----------------