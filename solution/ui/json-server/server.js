const jsonServer  = require("json-server");
const server      = jsonServer.create();
const router      = jsonServer.router(require("./db.js")());
const middlewares = jsonServer.defaults();

server.use(middlewares);
server.use(router);
server.listen(3000, function() {
  console.log("JSON Server is running... \nTo add a new JSON APi, enter in db/json and put your json file there");
});
