const BASE_URL = "";

export const ACTIVATE_USER_BEGIN = "ACTIVATE_USER_BEGIN";
export const activateUserBegin = () => ({
  type: ACTIVATE_USER_BEGIN,
});

export const ACTIVATE_USER_SUCCESS = "ACTIVATE_USER_SUCCESS";
export const activateUserSucceeded = () => ({
  type: ACTIVATE_USER_SUCCESS,
});

export const ACTIVATE_USER_ERROR = "ACTIVATE_USER_ERROR";
export const activateUserFailure = () => ({
  type: ACTIVATE_USER_ERROR,
});

export function activateUser(username, password, activationToken) {
  return (dispatch) => {
    dispatch(activateUserBegin());

    let data = {
      username,
      password,
      activationToken,
    };

    fetch(`${BASE_URL}/api/user/activate`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (response.status === 200) dispatch(activateUserSucceeded());
        else dispatch(activateUserFailure());
      })
      .catch((err) => {
        dispatch(activateUserFailure());
      });
  };
}

export const PASSWORD_RESET_BEGIN = "PASSWORD_RESET_BEGIN";
export const resetPasswordBegin = () => ({
  type: PASSWORD_RESET_BEGIN,
});

export const PASSWORD_RESET_SUCCESS = "PASSWORD_RESET_SUCCESS";
export const resetPasswordSucceeded = () => ({
  type: PASSWORD_RESET_SUCCESS,
});

export const PASSWORD_RESET_ERROR = "PASSWORD_RESET_ERROR";
export const resetPasswordFailure = () => ({
  type: PASSWORD_RESET_ERROR,
});

export function resetPassword(password, resetToken) {
  return (dispatch) => {
    dispatch(resetPasswordBegin());

    let data = {
      password,
      resetToken,
    };

    fetch(`${BASE_URL}/api/user/password/reset`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (response.status === 200) {
          dispatch(resetPasswordSucceeded());
        }
      })
      .catch((err) => {
        dispatch(resetPasswordFailure());
      });
  };
}

export const REQUEST_PASSWORD_RESET_BEGIN = "REQUEST_PASSWORD_RESET_BEGIN";
export const requestPasswordResetBegin = () => ({
  type: REQUEST_PASSWORD_RESET_BEGIN,
});

export const REQUEST_PASSWORD_RESET_SUCCESS = "REQUEST_PASSWORD_RESET_SUCCESS";
export const requestPasswordResetSucceeded = () => ({
  type: REQUEST_PASSWORD_RESET_SUCCESS,
});

export const REQUEST_PASSWORD_RESET_ERROR = "REQUEST_PASSWORD_RESET_ERROR";
export const requestPasswordResetFailure = (error) => ({
  type: REQUEST_PASSWORD_RESET_ERROR,
  error,
});

export function requestPasswordReset(email) {
  return (dispatch) => {
    dispatch(requestPasswordResetBegin());

    const data = {
      email,
    };

    fetch(`${BASE_URL}/api/user/password/forgot`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (response.status === 200) {
          dispatch(requestPasswordResetSucceeded());
        }
      })
      .catch((err) => {
        dispatch(requestPasswordResetFailure(err));
      });
  };
}
