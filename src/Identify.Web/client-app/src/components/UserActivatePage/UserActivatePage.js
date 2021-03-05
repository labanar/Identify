import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router";
import { activateUser } from "../../actions";
import Layout from "../shared/Layout";
import UserActivateForm from "./UserActivateForm";

export default function UserActivatePage() {
  const { token } = useParams();
  const dispatch = useDispatch();
  const isActivating = useSelector((state) => state.userReducer.isActivating);
  const isActivated = useSelector((state) => state.userReducer.isActivated);
  const [activateAttempted, setActivationAttempted] = useState(false);

  const userActivate = (username, password) => {
    setActivationAttempted(true);
    dispatch(activateUser(username, password, token));
  };

  return (
    <Layout>
      {!activateAttempted && (
        <UserActivateForm onSubmit={userActivate} isSubmitting={isActivating} />
      )}
      {activateAttempted && isActivated && "ACTIVATION SUCCESS"}
      {activateAttempted &&
        !isActivating &&
        !isActivated &&
        "ACTIVATION FAILURE"}
    </Layout>
  );
}
