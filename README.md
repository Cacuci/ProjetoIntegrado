# ProjetoIntegrado – Arquitetura de Software Distribuído

Exemplo de aplicativo (WMS – Sistema de Gerenciamento de Armazém) baseado em uma arquitetura simplificada de microsserviços e contêineres Docker.

### Visão geral da arquitetura

Este aplicativo utiliza .NET 7, sendo capaz de ser executado em contêineres Linux ou Windows, dependendo do seu host Docker. A arquitetura propõe uma implementação de arquitetura orientada a microsserviços com múltiplos microsserviços autônomos (cada um possuindo seus próprios dados/banco de dados) e implementando diferentes abordagens dentro de cada microsserviço (padrões simples CRUD vs. DDD/CQRS) usando HTTPs como protocolo de comunicação e os microsserviços suportam comunicação assíncrona para propagação de atualizações de dados em vários serviços baseados em eventos de integração e um barramento de eventos (um corretor de mensagens leves, para escolher entre RabbitMQ) 
