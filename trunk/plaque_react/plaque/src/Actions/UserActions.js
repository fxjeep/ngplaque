import { history } from '../Utils/history';
import Config from '../config';
import request, {post} from '../Utils/request';
import { UserActionConstant} from '../Reducers/authentication';

export const userActions = {
    login,
    showDetails,
    logout
};

function login(username, password) {
    return dispatch => {
        post(Config.LoginUrl, {"name": username, "password":password})
            .then(
                result => { 
                    sessionStorage.setItem(Config.SessionKey, result.data.token);
                    success(result.user);
                    history.push('/main');
                },
                error => {
                    dispatch(failure(error.toString()));
                    //dispatch(alertActions.error(error.toString()));
                }
            );
    };

    function request(user) { return { type: UserActionConstant.LOGIN_REQUEST, user } }
    function success(user) { return { type: UserActionConstant.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: UserActionConstant.LOGIN_FAILURE, error } }
}

function logout(){
    return dispatch=>{
        sessionStorage.setItem(Config.SessionKey, "");
        history.push('/');
    };
}



function showDetails(){
    return dispatch =>{
        history.push('/detail');
    };
}
