import {
  Box,
  Button,
  CircularProgress,
  makeStyles,
  Typography,
} from "@material-ui/core";
import { grey } from "@material-ui/core/colors";
import React, { useState } from "react";
import { useHistory } from "react-router";
import SharedTextField from "../shared/SharedTextField";

const useStyles = makeStyles((theme) => ({
  root: {
    background: "linear-gradient(to right bottom, #141e30, #345478)",
  },
  textField: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    color: grey[400],
    borderColor: grey[300],
    maxWidth: "360px",
  },
  signInButton: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    padding: theme.spacing(1),
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.primary.contrastText,
  },
  resetLink: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    color: grey[300],
  },
  loginSpinner: {
    color: grey[300],
  },
}));

export default function LoginForm({ onSubmit, isSubmitting }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const classes = useStyles();
  const history = useHistory();

  const renderForm = (classes) => {
    return (
      <Box
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"center"}
        minWidth={"25%"}
        p={3}
      >
        <SharedTextField
          id="username"
          label={"Username"}
          className={classes.textField}
          value={username}
          setValue={setUsername}
        />
        <SharedTextField
          id="password"
          label={"Password"}
          className={classes.textField}
          value={password}
          setValue={setPassword}
          password
        />
        <div className={classes.resetLink}>
          <Typography variant={"caption"}>{"Forgot your password?"}</Typography>
          <span
            style={{
              marginLeft: 5,
              color: "white",
              cursor: "grab",
              textDecoration: "underline",
            }}
            onClick={() => {
              history.push("/forgot-password");
            }}
          >
            <Typography variant={"caption"}>{"Click Here"}</Typography>
          </span>
        </div>
        <Button
          disabled={isSubmitting}
          variant={"contained"}
          className={classes.signInButton}
          onClick={() => onSubmit(username, password)}
        >
          Sign in
        </Button>
      </Box>
    );
  };

  const renderLoading = (classes) => {
    return (
      <Box
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"center"}
        alignItems={"center"}
        className={classes.loginSpinner}
      >
        <Box style={{ color: grey[300] }}>
          <CircularProgress color={"inherit"} size={20} />
        </Box>
        <Box>{"Logging in..."}</Box>
      </Box>
    );
  };

  return (
    <>
      {!isSubmitting && renderForm(classes)}
      {isSubmitting && renderLoading(classes)}
    </>
  );
}
