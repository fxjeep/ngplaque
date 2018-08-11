import { combineReducers } from 'redux';
import { authentication } from './authentication';
import { mainPanel } from './mainPanelReducer';
import { editDetails } from './editDetailsReducer';


const rootReducer = combineReducers({
  authentication,
  mainPanel,
  editDetails
});

export default rootReducer;   