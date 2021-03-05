const BASE_URL = "";

export const LOGIN_BEGIN = "LOGIN_BEGIN";
export const loginBegin = () => ({
  type: LOGIN_BEGIN,
});

export const LOGIN_SUCCESS = "LOGIN_SUCCESS";
export const loginSucceeded = (redirectUrl) => ({
  type: LOGIN_SUCCESS,
  redirectUrl,
});

export const LOGIN_ERROR = "LOGIN_ERROR";
export const loginFailure = () => ({
  type: LOGIN_ERROR,
});

export function login(username, password, returnUrl) {
  return (dispatch) => {
    dispatch(loginBegin());

    const data = {
      username,
      password,
      returnUrl,
    };

    fetch(`${BASE_URL}/api/authenticate`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((json) => dispatch(loginSucceeded(json.data.redirectUrl)))
      .catch((err) => dispatch(loginFailure()));
  };
}

export const FETCH_CONSENT_SUMMARY_BEGIN = "FETCH_CONSENT_SUMMARY_BEGIN";
export const fetchConsentSummaryBegin = () => ({
  type: FETCH_CONSENT_SUMMARY_BEGIN,
});

export const FETCH_CONSENT_SUMMARY_SUCCESS = "FETCH_CONSENT_SUMMARY_SUCCESS";
export const fetchConsentSummarySucceeded = (summary) => ({
  type: FETCH_CONSENT_SUMMARY_SUCCESS,
  summary,
});

export const FETCH_CONSENT_SUMMARY_ERROR = "FETCH_CONSENT_SUMMARY_ERROR";
export const fetchConsentSummaryFailure = () => ({
  type: FETCH_CONSENT_SUMMARY_ERROR,
});

export function fetchConsentSummary(scopes, clientId) {
  return (dispatch) => {
    dispatch(fetchConsentSummaryBegin());

    fetch(
      `${BASE_URL}/api/authenticate/consent?clientId=${clientId}&scopes=${scopes.join(
        ","
      )}`,
      {
        method: "GET",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      }
    )
      .then((response) => response.json())
      .then((json) => dispatch(fetchConsentSummarySucceeded(json.data)))
      .catch((err) => dispatch(fetchConsentSummaryFailure()));
  };
}

export const GRANT_CONSENT_BEGIN = "GRANT_CONSENT_BEGIN";
export const grantConsentBegin = () => ({
  type: GRANT_CONSENT_BEGIN,
});

export const GRANT_CONSENT_SUCCESS = "GRANT_CONSENT_SUCCESS";
export const grantConsentSucceeded = (redirectUrl) => ({
  type: GRANT_CONSENT_SUCCESS,
  redirectUrl,
});

export const GRANT_CONSENT_ERROR = "GRANT_CONSENT_ERROR";
export const grantConsentFailure = () => ({
  type: GRANT_CONSENT_ERROR,
});

export function grantConsent(returnUrl) {
  return (dispatch) => {
    dispatch(grantConsentBegin());

    const data = {
      returnUrl,
    };

    fetch(`${BASE_URL}/api/authenticate/consent`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((json) => dispatch(grantConsentSucceeded(json.data.redirectUrl)))
      .catch((err) => dispatch(grantConsentFailure()));
  };
}
