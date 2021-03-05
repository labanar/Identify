import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router";
import { login } from "../../actions";
import Layout from "../shared/Layout";
import LoginForm from "./LoginForm";

export default function LoginPage(props) {
  const dispatch = useDispatch();
  const history = useHistory();
  const location = useLocation();
  const redirectUrl = useSelector(
    (state) => state.authReducer.loginRedirectUrl
  );
  const isLoggingIn = useSelector((state) => state.authReducer.isLoggingIn);
  const returnUrl = location.search.replace("?ReturnUrl=", "");

  const userLogin = (username, password) => {
    dispatch(login(username, password, returnUrl));
  };

  useEffect(() => {
    if (redirectUrl && redirectUrl.length > 0) {
      history.push(redirectUrl);
      history.go(0);
    }
  }, [redirectUrl]);

  return (
    <Layout>
      <LoginForm onSubmit={userLogin} isSubmitting={isLoggingIn} />
    </Layout>
  );
}
