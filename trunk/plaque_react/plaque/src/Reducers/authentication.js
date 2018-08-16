export const UserActionConstant = {
  LOGIN_REQUEST:"LOGIN_REQUEST",
  LOGIN_SUCCESS:"LOGIN_SUCCESS",
  LOGIN_FAILURE:"LOGIN_FAILURE"
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
    case "LOGIN":
      return {
        loggingIn: true,
        user: action.user
      };
    case UserActionConstant.LOGIN_SUCCESS:
      return {
        loggedIn: true,
        user: action.user
      };
    case "LOGIN_FAILURE":
      return {loggedIn: false,
        error: action.error};
    case "LOGOUT":
      return {};
    default:
      return state
  }
}

const mainPanelState = {};
export function mainpanel(state=mainPanelState, action){
    switch(action.type){
      
    }
}