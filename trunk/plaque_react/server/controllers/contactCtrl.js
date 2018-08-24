const Contact = require('../models/contact');
const Ancester = require('../models/ancestor');
const Dead = require('../models/dead');
const Live = require('../models/live');

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

const update = async function(req, res){
    res.setHeader('Content-Type', 'application/json');
    var ContactObj = new Contact(req.body);
    if (!ContactObj.id){
        return ReError(res, "id is empty in contact", 422);
    }
    ContactObj.findOneAndUpdate({_id:ContactObj.id}, ContactObj)
            .then(function(data){
                return ReSucess(res, data);
            })
            .catch((error)=>{
                return ReError(res, error);
            });
}
module.exports.update = update;

const deleteOne = async function(req, res){
    res.setHeader('Content-Type', 'application/json');
    var id = req.body.id;
    if (!id){
        return ReError(res, "id is empty in contact", 422);
    }
    ContactObj.remove({_id:id})
            .then(function(data){
                return ReSucess(res, data);
            })
            .catch((error)=>{
                return ReError(res, error);
            });
}
module.exports.delete = deleteOne;

const getDetails = async function(req, res){
    res.setHeader('Content-Type', 'application/json');
    var id = req.body.id;
    if (!id){
        return ReError(res, "id is empty in contact", 422);
    }
    let result = {
        ancester:[],
        live:[],
        deceased:[]
    };
    Ancester.findOne({contactId:id}).exec()
            .then(function(ancesterFromDB){
                result.ancester = ancesterFromDB;
                return Dead.findOne({contactId:id}).exec();
            })
            .then(function(deadFromDB){
                result.deceased = deadFromDB;
                return Live.findOne({contactId:id}).exec();
            })
            .then(function(liveFromDB){
                result.live = liveFromDB;
                return ReSucess(res, result);
            })
            .catch((error)=>{
                return ReError(res, error);
            });
}
module.exports.getDetails = getDetails;
