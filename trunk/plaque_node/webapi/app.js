var path = require('path'),
    express = require('express'),
    http = require('http'),
    csrf = require('csurf'),
    bodyParser = require('body-parser'),
    cookieParser = require('cookie-parser'),
    mongoose = require('mongoose'),
    uristring = 'mongodb://linkspeed:link1234@plaque-shard-00-00-nxu5c.mongodb.net:27017,'+
    'plaque-shard-00-01-nxu5c.mongodb.net:27017,'+
    'plaque-shard-00-02-nxu5c.mongodb.net:27017'+
    '/test?ssl=true&replicaSet=plaque-shard-0&authSource=admin',
    compression = require('compression'),
    port = process.env.PORT || 8001;

var app = express();

// Switch off the default 'X-Powered-By: Express' header
app.disable('x-powered-by');

// compress all requests
app.use(compression());

/* Makes connection asynchronously.  Mongoose will queue up database
operations and release them when the connection is complete. */
mongoose.connect(uristring, function (err, res) {
  if (err) {
  console.log ('ERROR connecting to: ' + uristring + '. ' + err);
  } else {
  console.log ('Succeeded connected to: ' + uristring);
  }
});

app.use(bodyParser.urlencoded({extended: false}));
app.use(cookieParser());
app.use(csrf());
// error handler for csrf tokens
app.use(function (err, req, res, next) {
    if (err.code !== 'EBADCSRFTOKEN') {
        return next(err);
    }
    // handle CSRF token errors here
    res.status(403);
    res.send('Session has expired or form tampered with.');
})

app.use(express.static(path.join(__dirname, 'public')));

// Configure passport
http.createServer(app).listen(port);