const Contact = require('../models/contact');
const authService   = require('../services/authsvc');
const { to, ReError, ReSucess }  = require('../services/utilssvc');


const list = async function(req, res){
    res.setHeader('Content-Type', 'application/json');
    Contact.find()
            .then(contacts => {
                return ReSucess(res, {contacts:contacts});
            }).catch(err => {
                res.status(500).send({
                    message: err.message || "failed to retrieve contacts."
                });
            });
}
module.exports.list = list;

const add = async function(req, res){
    res.setHeader('Content-Type', 'application/json');
    var ContactObj = new Contact(req.body);
    ContactObj.save()
            .then((data)=>{
                var contactReturn = {};
                contactReturn.name = data.name;
                contactReturn.code = data.code;
                contactReturn.lastPrint = data.lastPrint;
                contactReturn.id = data._id;
                return ReSucess(res, contactReturn);
            })
            .catch((error)=>{
                return ReError(res, error);
            });
}
module.exports.add = add;