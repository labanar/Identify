import { makeStyles } from "@material-ui/core";
import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router";
import ConsentForm from "./ConsentForm";

const useStyles = makeStyles((theme) => ({
  root: {
    background: "linear-gradient(to right bottom, #141e30, #345478)",
  },
}));

export default function ConsentPage() {
  const classes = useStyles();
  const { token } = useParams();
  const dispatch = useDispatch();
  const isActivating = useSelector((state) => state.userReducer.isActivating);
  const isActivated = useSelector((state) => state.userReducer.isActivated);
  const [activateAttempted, setActivationAttempted] = useState(false);

  const consentToScopes = (uscopes) => {
    window.alert(token);
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
      className={classes.root}
    >
      <ConsentForm onSubmit={consentToScopes} />
    </div>
  );
}
