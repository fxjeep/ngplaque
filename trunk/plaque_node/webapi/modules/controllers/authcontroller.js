var BaseController = require("./basecontroller");
const UserManager = require('../managers/userManager'); 

class AuthController extends BaseController {
    constructor() {
        super();
    }

    // Request token by credentials
    create(req, res, next) {
        
        var email = req.body.email;
        var password = req.body.password;
        UserManager.checkUserLogin(email, password, (user) => {
            console.log("loggedin");
        });
    }

    // Revoke Token
    remove(req, res, next) {
        let responseManager = this._responseManager;
        let that = this;
        this._passport.authenticate('jwt-rs-auth', {
            onVerified: function (token, user) {
                that._authHandler.revokeToken(req, token, responseManager.getDefaultResponseHandler(res));
            },
            onFailure: function (error) {
                responseManager.respondWithError(res, error.status || 401, error.message);
            }
        })(req, res, next);

    }
}

module.exports = AuthController;