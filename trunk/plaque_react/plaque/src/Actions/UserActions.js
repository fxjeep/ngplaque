import { history } from '../Utils/history';

export const userActions = {
    login,
    showDetails
};

function login(username, password) {
    return dispatch => {
        history.push('/main');
        dispatch({ type: "LOGIN_SUCCESS", username });
        
        // userService.login(username, password)
        //     .then(
        //         user => { 
        //             dispatch(success(user)); 
        //             history.push('/');
        //         },
        //         error => {
        //             dispatch(failure(error.toString()));
        //             dispatch(alertActions.error(error.toString()));
        //         }
        //     );
    };

    function request(user) { return { type: "LOGIN_REQUEST", user } }
    function success(user) { return { type: "LOGIN_SUCCESS", user } }
    function failure(error) { return { type: "LOGIN_FAILURE", error } }
}

function showDetails(){
    return dispatch =>{
        history.push('/detail');
    };
}
