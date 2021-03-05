import React, { Component } from "react";
import { Route } from "react-router";
import LoginPage from "./components/LoginPage/LoginPage";
import UserActivatePage from "./components/UserActivatePage/UserActivatePage";
import UserPasswordResetPage from "./components/UserPasswordResetPage/UserPasswordResetPage";
import UserForgotPasswordPage from "./components/UserForgotPasswordPage/UserForgotPasswordPage";
import { Provider } from "react-redux";
import configureStore from "./configureStore";
import { BrowserRouter } from "react-router-dom";
import ConsentPage from "./components/ConsentPage/ConsentPage";

const store = configureStore();
export default class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <BrowserRouter>
          <Route exact path="/" component={LoginPage} />
          <Route path="/login" component={LoginPage} />
          <Route path="/consent" component={ConsentPage} />
          <Route
            exact
            path={"/reset/:token"}
            render={() => <UserPasswordResetPage />}
          />
          <Route
            exact
            path={"/activate/:token"}
            render={() => <UserActivatePage />}
          />
          <Route
            exact
            path={"/forgot-password"}
            render={() => <UserForgotPasswordPage />}
          />
        </BrowserRouter>
      </Provider>
    );
  }
}
