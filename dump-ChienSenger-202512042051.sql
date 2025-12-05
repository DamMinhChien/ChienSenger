--
-- PostgreSQL database dump
--

\restrict I0BvJWKWct0Yn7RXchZJQ2C55XriNQufaoMAbkvmbg1YaPzFwdJ3eA1th7nvcPt

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

-- Started on 2025-12-04 20:51:38

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 878 (class 1247 OID 16486)
-- Name: friend_status; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.friend_status AS ENUM (
    'pending',
    'accepted',
    'rejected'
);


ALTER TYPE public.friend_status OWNER TO postgres;

--
-- TOC entry 869 (class 1247 OID 16438)
-- Name: message_status; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.message_status AS ENUM (
    'sent',
    'delivered',
    'read',
    'sending',
    'failed',
    'deleted',
    'recalled'
);


ALTER TYPE public.message_status OWNER TO postgres;

--
-- TOC entry 866 (class 1247 OID 16432)
-- Name: message_type; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.message_type AS ENUM (
    'text',
    'image'
);


ALTER TYPE public.message_type OWNER TO postgres;

--
-- TOC entry 884 (class 1247 OID 16534)
-- Name: user_role; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.user_role AS ENUM (
    'admin',
    'user'
);


ALTER TYPE public.user_role OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 222 (class 1259 OID 16406)
-- Name: conversations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.conversations (
    id integer NOT NULL,
    user1_id integer NOT NULL,
    user2_id integer NOT NULL,
    last_message text,
    updated_at timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.conversations OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 16405)
-- Name: conversations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.conversations_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.conversations_id_seq OWNER TO postgres;

--
-- TOC entry 5094 (class 0 OID 0)
-- Dependencies: 221
-- Name: conversations_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.conversations_id_seq OWNED BY public.conversations.id;


--
-- TOC entry 227 (class 1259 OID 16494)
-- Name: friends; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.friends (
    id integer NOT NULL,
    user_id integer NOT NULL,
    friend_id integer NOT NULL,
    status public.friend_status DEFAULT 'pending'::public.friend_status NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.friends OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 16493)
-- Name: friends_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.friends_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.friends_id_seq OWNER TO postgres;

--
-- TOC entry 5096 (class 0 OID 0)
-- Dependencies: 226
-- Name: friends_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.friends_id_seq OWNED BY public.friends.id;


--
-- TOC entry 224 (class 1259 OID 16446)
-- Name: messages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.messages (
    id integer NOT NULL,
    conversation_id integer NOT NULL,
    sender_id integer NOT NULL,
    type public.message_type DEFAULT 'text'::public.message_type NOT NULL,
    content text NOT NULL,
    "timestamp" timestamp without time zone DEFAULT now() NOT NULL,
    status public.message_status DEFAULT 'sent'::public.message_status NOT NULL
);


ALTER TABLE public.messages OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16445)
-- Name: messages_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.messages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.messages_id_seq OWNER TO postgres;

--
-- TOC entry 5098 (class 0 OID 0)
-- Dependencies: 223
-- Name: messages_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.messages_id_seq OWNED BY public.messages.id;


--
-- TOC entry 225 (class 1259 OID 16472)
-- Name: user_status; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_status (
    user_id integer NOT NULL,
    is_online boolean DEFAULT false NOT NULL,
    last_seen timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.user_status OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16391)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    password_hash text NOT NULL,
    display_name character varying(100),
    avatar_url text,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    role public.user_role DEFAULT 'user'::public.user_role NOT NULL,
    is_locked boolean DEFAULT false NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16390)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_id_seq OWNER TO postgres;

--
-- TOC entry 5101 (class 0 OID 0)
-- Dependencies: 219
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- TOC entry 4892 (class 2604 OID 16409)
-- Name: conversations id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conversations ALTER COLUMN id SET DEFAULT nextval('public.conversations_id_seq'::regclass);


--
-- TOC entry 4900 (class 2604 OID 16497)
-- Name: friends id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends ALTER COLUMN id SET DEFAULT nextval('public.friends_id_seq'::regclass);


--
-- TOC entry 4894 (class 2604 OID 16449)
-- Name: messages id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.messages ALTER COLUMN id SET DEFAULT nextval('public.messages_id_seq'::regclass);


--
-- TOC entry 4888 (class 2604 OID 16394)
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- TOC entry 5081 (class 0 OID 16406)
-- Dependencies: 222
-- Data for Name: conversations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.conversations (id, user1_id, user2_id, last_message, updated_at) FROM stdin;
\.


--
-- TOC entry 5086 (class 0 OID 16494)
-- Dependencies: 227
-- Data for Name: friends; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.friends (id, user_id, friend_id, status, created_at) FROM stdin;
\.


--
-- TOC entry 5083 (class 0 OID 16446)
-- Dependencies: 224
-- Data for Name: messages; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.messages (id, conversation_id, sender_id, type, content, "timestamp", status) FROM stdin;
\.


--
-- TOC entry 5084 (class 0 OID 16472)
-- Dependencies: 225
-- Data for Name: user_status; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.user_status (user_id, is_online, last_seen) FROM stdin;
\.


--
-- TOC entry 5079 (class 0 OID 16391)
-- Dependencies: 220
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, username, password_hash, display_name, avatar_url, created_at, role, is_locked) FROM stdin;
\.


--
-- TOC entry 5102 (class 0 OID 0)
-- Dependencies: 221
-- Name: conversations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.conversations_id_seq', 1, false);


--
-- TOC entry 5103 (class 0 OID 0)
-- Dependencies: 226
-- Name: friends_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.friends_id_seq', 1, false);


--
-- TOC entry 5104 (class 0 OID 0)
-- Dependencies: 223
-- Name: messages_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.messages_id_seq', 1, false);


--
-- TOC entry 5105 (class 0 OID 0)
-- Dependencies: 219
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 1, false);


--
-- TOC entry 4908 (class 2606 OID 16415)
-- Name: conversations conversations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conversations
    ADD CONSTRAINT conversations_pkey PRIMARY KEY (id);


--
-- TOC entry 4910 (class 2606 OID 16417)
-- Name: conversations conversations_user1_id_user2_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conversations
    ADD CONSTRAINT conversations_user1_id_user2_id_key UNIQUE (user1_id, user2_id);


--
-- TOC entry 4921 (class 2606 OID 16502)
-- Name: friends friends_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_pkey PRIMARY KEY (id);


--
-- TOC entry 4923 (class 2606 OID 16504)
-- Name: friends friends_user_id_friend_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_user_id_friend_id_key UNIQUE (user_id, friend_id);


--
-- TOC entry 4917 (class 2606 OID 16459)
-- Name: messages messages_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_pkey PRIMARY KEY (id);


--
-- TOC entry 4919 (class 2606 OID 16479)
-- Name: user_status user_status_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_status
    ADD CONSTRAINT user_status_pkey PRIMARY KEY (user_id);


--
-- TOC entry 4904 (class 2606 OID 16402)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 4906 (class 2606 OID 16404)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4911 (class 1259 OID 16430)
-- Name: idx_conversation_updated_at; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_conversation_updated_at ON public.conversations USING btree (updated_at DESC);


--
-- TOC entry 4912 (class 1259 OID 16428)
-- Name: idx_conversation_user1; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_conversation_user1 ON public.conversations USING btree (user1_id);


--
-- TOC entry 4913 (class 1259 OID 16429)
-- Name: idx_conversation_user2; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_conversation_user2 ON public.conversations USING btree (user2_id);


--
-- TOC entry 4914 (class 1259 OID 16470)
-- Name: idx_message_conversation; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_message_conversation ON public.messages USING btree (conversation_id);


--
-- TOC entry 4915 (class 1259 OID 16471)
-- Name: idx_message_timestamp; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_message_timestamp ON public.messages USING btree ("timestamp");


--
-- TOC entry 4924 (class 2606 OID 16418)
-- Name: conversations conversations_user1_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conversations
    ADD CONSTRAINT conversations_user1_id_fkey FOREIGN KEY (user1_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 4925 (class 2606 OID 16423)
-- Name: conversations conversations_user2_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.conversations
    ADD CONSTRAINT conversations_user2_id_fkey FOREIGN KEY (user2_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 4929 (class 2606 OID 16510)
-- Name: friends friends_friend_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_friend_id_fkey FOREIGN KEY (friend_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 4930 (class 2606 OID 16505)
-- Name: friends friends_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.friends
    ADD CONSTRAINT friends_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 4926 (class 2606 OID 16460)
-- Name: messages messages_conversation_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_conversation_id_fkey FOREIGN KEY (conversation_id) REFERENCES public.conversations(id) ON DELETE CASCADE;


--
-- TOC entry 4927 (class 2606 OID 16465)
-- Name: messages messages_sender_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_sender_id_fkey FOREIGN KEY (sender_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 4928 (class 2606 OID 16480)
-- Name: user_status user_status_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_status
    ADD CONSTRAINT user_status_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) ON DELETE CASCADE;


--
-- TOC entry 5092 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT USAGE ON SCHEMA public TO my_user;


--
-- TOC entry 5093 (class 0 OID 0)
-- Dependencies: 222
-- Name: TABLE conversations; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.conversations TO my_user;


--
-- TOC entry 5095 (class 0 OID 0)
-- Dependencies: 227
-- Name: TABLE friends; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.friends TO my_user;


--
-- TOC entry 5097 (class 0 OID 0)
-- Dependencies: 224
-- Name: TABLE messages; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.messages TO my_user;


--
-- TOC entry 5099 (class 0 OID 0)
-- Dependencies: 225
-- Name: TABLE user_status; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.user_status TO my_user;


--
-- TOC entry 5100 (class 0 OID 0)
-- Dependencies: 220
-- Name: TABLE users; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.users TO my_user;


--
-- TOC entry 2083 (class 826 OID 16554)
-- Name: DEFAULT PRIVILEGES FOR TABLES; Type: DEFAULT ACL; Schema: public; Owner: postgres
--

ALTER DEFAULT PRIVILEGES FOR ROLE postgres IN SCHEMA public GRANT ALL ON TABLES TO my_user;


-- Completed on 2025-12-04 20:51:38

--
-- PostgreSQL database dump complete
--

\unrestrict I0BvJWKWct0Yn7RXchZJQ2C55XriNQufaoMAbkvmbg1YaPzFwdJ3eA1th7nvcPt

