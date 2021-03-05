import React, { useState } from "react";
import { blue, grey } from "@material-ui/core/colors";
import { Box, makeStyles, Paper, TextField, Button } from "@material-ui/core";
import SharedTextField from "../shared/SharedTextField";

const useStyles = makeStyles((theme) => ({
  root: {
    background: "linear-gradient(to right bottom, #141e30, #345478)",
  },
  textField: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    color: "black",
  },

  resetButton: {
    width: "100%",
    backgroundColor: blue[500],
    "&:hover": {
      backgroundColor: blue[800],
    },
    color: "white",
  },
  resetSpinner: {
    color: grey[300],
  },
  resetCheck: {
    color: grey[300],
    "-webkit-text-stroke": "2px #e0e0e0",
  },
}));

export default function UserPasswordResetForm({ onSubmit, isSubmitting }) {
  const classes = useStyles();

  const [password, setPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");

  const renderForm = (classes) => {
    return (
      <Box
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"center"}
        minWidth={"35%"}
        maxWidth={"95%"}
      >
        <Paper>
          <Box
            display={"flex"}
            flexDirection={"column"}
            justifyContent={"center"}
            p={3}
          >
            <Box mt={1} fontWeight={400} fontSize={20} color={"black"}>
              {"Reset Password"}
            </Box>
            <Box mt={1.5} fontWeight={300} fontSize={15} color={"black"}>
              <p>
                Enter a new password. Your new password must meet the following
                requirements:
                <ul>
                  <li>{"Contains at least one upper-case character"}</li>
                  <li>{"Contains at least one lower-case character"}</li>
                  <li>{"Contains at least one special character"}</li>
                  <li>{"Contains at least one number"}</li>
                </ul>
              </p>
            </Box>
            <Box mt={2}>
              <Box>
                <TextField
                  fullWidth
                  className={classes.textField}
                  id={"password"}
                  label={"Password"}
                  type="password"
                  value={password}
                  variant={"outlined"}
                  onChange={(event) => setPassword(event.target.value)}
                />
              </Box>
              <Box>
                <TextField
                  fullWidth
                  className={classes.textField}
                  id={"repeat-password"}
                  label={"Repeat Password"}
                  type="password"
                  value={repeatPassword}
                  variant={"outlined"}
                  onChange={(event) => setRepeatPassword(event.target.value)}
                />
              </Box>
            </Box>
            <Box mt={3} display={"flex"}>
              <Box width={"100%"} mr={0.5}>
                <Button
                  variant={"text"}
                  className={classes.resetButton}
                  onClick={() => onSubmit(password)}
                >
                  Reset Password
                </Button>
              </Box>
            </Box>
          </Box>
        </Paper>
      </Box>
    );
  };

  const renderLoading = () => {};

  return (
    <>
      {!isSubmitting && renderForm(classes)}
      {isSubmitting && renderLoading()}
    </>
  );
}
