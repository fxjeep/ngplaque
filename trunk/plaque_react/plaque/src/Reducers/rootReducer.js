import { combineReducers } from 'redux';
import { authentication } from './authentication';
import { mainPanel } from './mainPanelReducer';


const rootReducer = combineReducers({
  authentication,
  mainPanel
});

export default rootReducer;   