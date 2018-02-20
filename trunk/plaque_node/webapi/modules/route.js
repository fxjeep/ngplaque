const express = require('express'),
const AuthController = require('./controllers/authcontroller');

router = express.Router();

//login controller
let authController = new AuthController();
router.post('/auth/', authController.create);
router.delete('auth/:token', authController.remove);


module.exports = router;