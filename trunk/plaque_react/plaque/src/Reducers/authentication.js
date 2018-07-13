let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export function authentication(state = initialState, action) {
  switch (action.type) {
    case "LOGIN":
      return {
        loggingIn: true,
        user: action.user
      };
    case "LOGIN_SUCCESS":
      return {
        loggedIn: true,
        user: action.user
      };
    case "LOGIN_FAILURE":
      return {};
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