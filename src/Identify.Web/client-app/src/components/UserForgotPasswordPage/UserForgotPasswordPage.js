import React from "react";
import Layout from "../shared/Layout";
import { useDispatch } from "react-redux";
import { requestPasswordReset } from "../../actions";
import { UserForgotPasswordForm } from "./UserForgotPasswordForm";

export default function UserForgotPasswordPage(props) {
  const dispatch = useDispatch();

  const userRequestPasswordReset = (email) => {
    dispatch(requestPasswordReset(email));
  };

  return (
    <Layout>
      <UserForgotPasswordForm
        onSubmit={userRequestPasswordReset}
        isSubmitting={false}
      />
    </Layout>
  );
}
