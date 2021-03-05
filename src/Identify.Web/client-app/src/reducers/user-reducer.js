import {
  ACTIVATE_USER_BEGIN,
  ACTIVATE_USER_SUCCESS,
  ACTIVATE_USER_ERROR,
  PASSWORD_RESET_SUCCESS,
  PASSWORD_RESET_ERROR,
  PASSWORD_RESET_BEGIN,
} from "../actions";

const initialState = {
  isActivating: false,
  isActivated: false,
  isResettingPassword: false,
};

export default function userReducer(state = initialState, action) {
  switch (action.type) {
    case ACTIVATE_USER_BEGIN:
      return {
        ...state,
        isActivating: true,
        isActivated: false,
      };
    case ACTIVATE_USER_SUCCESS:
      return {
        ...state,
        isActivating: false,
        isActivated: true,
      };
    case ACTIVATE_USER_ERROR:
      return {
        ...state,
        isActivating: false,
        isActivated: false,
      };
    case PASSWORD_RESET_BEGIN:
      return {
        ...state,
        isResettingPassword: true,
      };
    case PASSWORD_RESET_SUCCESS:
      return {
        ...state,
        isResettingPassword: false,
      };
    case PASSWORD_RESET_ERROR:
      return {
        ...state,
        isResettingPassword: false,
      };
    default:
      return state;
  }
}
