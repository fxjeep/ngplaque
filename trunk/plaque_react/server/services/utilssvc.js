const {to} = require('await-to-js');
const pe = require('parse-error');

module.exports.to = async (promise) => {
    let err, res;
    [err, res] = await to(promise);
    if(err) return [pe(err)];

    return [null, res];
};

module.exports.ReError = function(res, err, code){ // Error Web Response
    var send_data = { error:'',
                      status:'' };

    if(typeof err == 'object' && typeof err.message != 'undefined') err = err.message;
    if(typeof code !== 'undefined') res.statusCode = code;
    return res.json({success:false, error: err});
};

module.exports.ReSucess = function(res, data, code) { // Success Web Response
    let send_data = {success:true,
                        data:'',
                        status:''};

    if(typeof data != 'undefined') send_data.data = data;
    if(typeof code !== 'undefined') res.statusCode = code;
    return res.json(send_data)
};

module.exports.ThrowError = function(err_message, log) { // TE stands for Throw Error
    if(log === true) console.error(err_message);    
    throw new Error(err_message);
};

