import { history } from '../Utils/history';

export const UserActionConstant = {
  LOGIN_REQUEST:"LOGIN_REQUEST",
  LOGIN_SUCCESS:"LOGIN_SUCCESS",
  LOGIN_FAILURE:"LOGIN_FAILURE",
  LOGIN_OUT:"LOGIN_OUT"
};


const initialState = {
  error:'',
  loggedIn:false,
  user: null
};

let user = JSON.parse(localStorage.getItem('user'));
if (user){
  initialState.loggedIn =true;
  initialState.user = user;
}

export function authentication(state = initialState, action) {
  switch (action.type) {
    case UserActionConstant.LOGIN_SUCCESS:
      return {
        loggedIn: true,
        user: action.user
      };
    case UserActionConstant.LOGIN_FAILURE:
      return {loggedIn: false,
        error: action.error};
    case UserActionConstant.LOGIN_OUT:
      return {loggedIn:false, error:''};
    default:
      return state
  }
}

const mainPanelState = {};
export function mainpanel(state=mainPanelState, action){
    switch(action.type){
      
    }
}