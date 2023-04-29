using ChatServerApp;

var server = new ChatServer(8888);
await server.StartAsync();