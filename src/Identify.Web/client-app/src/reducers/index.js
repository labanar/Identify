import { combineReducers } from "redux";
import authReducer from "./auth-reducer";
import userReducer from "./user-reducer";

export default combineReducers({
  authReducer: authReducer,
  userReducer: userReducer
});
