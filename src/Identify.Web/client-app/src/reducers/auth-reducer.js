import {
    LOGIN_SUCCESS,
    FETCH_CONSENT_SUMMARY_BEGIN,
    FETCH_CONSENT_SUMMARY_SUCCESS,
    FETCH_CONSENT_SUMMARY_ERROR,
    LOGIN_BEGIN,
    LOGIN_ERROR,
    GRANT_CONSENT_BEGIN,
    GRANT_CONSENT_SUCCESS,
    GRANT_CONSENT_ERROR
  } from "../actions";
  
  const initialState = {
    isFetchingConsentSummary: false,
    isLoggingIn: false,
    isGrantingConsent: false,
    consentSummary: {},
    consentRedirectUrl: "",
    loginRedirectUrl: ""
  };
  
  export default function authReducer(state = initialState, action) {
    switch (action.type) {
      case LOGIN_BEGIN: {
        return {
          ...state,
          isLoggingIn: true,
          loginRedirectUrl: ""
        };
      }
      case LOGIN_ERROR: {
        return {
          ...state,
          isLoggingIn: false
        };
      }
      case LOGIN_SUCCESS: {
        return {
          ...state,
          isLoggingIn: false,
          loginRedirectUrl: action.redirectUrl
        };
      }
      case GRANT_CONSENT_BEGIN: {
        return {
          ...state,
          isGrantingConsent: true,
          consentRedirectUrl: ""
        };
      }
      case GRANT_CONSENT_SUCCESS: {
        return {
          ...state,
          isGrantingConsent: false,
          consentRedirectUrl: action.redirectUrl
        };
      }
      case GRANT_CONSENT_ERROR: {
        return {
          ...state,
          isGrantingConsent: false
        };
      }
      case FETCH_CONSENT_SUMMARY_BEGIN: {
        return {
          ...state,
          isFetchingConsentSummary: true
        };
      }
  
      case FETCH_CONSENT_SUMMARY_SUCCESS: {
        return {
          ...state,
          isFetchingConsentSummary: false,
          consentSummary: action.summary
        };
      }
      case FETCH_CONSENT_SUMMARY_ERROR: {
        return state;
      }
      default:
        return state;
    }
  }