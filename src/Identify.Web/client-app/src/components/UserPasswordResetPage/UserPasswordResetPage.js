import React from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router";
import { resetPassword } from "../../actions";
import UserPasswordResetForm from "./UserPasswordResetForm";
import Layout from "../shared/Layout";

export default function UserPasswordResetPage() {
  const { token } = useParams();
  const dispatch = useDispatch();

  const userResetPassword = (password) => {
    dispatch(resetPassword(password, token));
  };

  return (
    <Layout>
      <UserPasswordResetForm
        onSubmit={userResetPassword}
        isSubmitting={false}
      />
    </Layout>
  );
}
