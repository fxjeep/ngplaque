var moment = require('moment');
const User = require('../models/user');
const userController   = require('../controllers/userCtrl');
const contactController   = require('../controllers/contactCtrl');
const authSvc = require('../services/authsvc');

function ensureAuthenticated(req, res, next) {
  if (!req.headers.authorization) { return res.status(401).send({ error: 'TokenMissing' });  }

  var token = req.headers.authorization.split(' ')[1];
  var payload = authSvc.checkJWT(token);
  if (payload == "") {
    return res.status(401).send({ error: "TokenInvalid" });
  }
  else if (payload.exp <= moment().unix()) {
    return res.status(401).send({ error: 'TokenExpired' });
  }
  // check if the user exists
  User.findById(payload.user_id, function(err, user){
    if (!user){
      return res.status(401).send({error: 'User or password is wrong'});
    } else {
      req.user = payload.user_id;
      next();
    }
  });
};

function defaultRoutes(app){
    app.get('/', function(req, res){
        res.statusCode = 200;//send the appropriate status code
        res.json({status:"success", message:"root", data:{}})
    });
}

// 3. Routes
module.exports = function (app) {
    app.post('/auth/login', userController.login);
    app.post('/auth/signup', userController.create);
    app.get('/contacts', ensureAuthenticated, contactController.list);
    //app.post('/contacts', ensureAuthenticated, contactController.add);
    app.post('/contacts', contactController.add);

    //app.get('/contact/{id}', ensureAuthenticated, contactController.get);
    defaultRoutes(app);
    //app.post('/auth/logout', userController.);
    //app.get('/contracts', ensureAuthenticated, contacts.list);
};