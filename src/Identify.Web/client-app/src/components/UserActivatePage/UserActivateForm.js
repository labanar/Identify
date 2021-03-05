import React, { useState } from "react";
import { blue, grey } from "@material-ui/core/colors";
import { Box, makeStyles, Paper, TextField, Button } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
  textField: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    color: "black",
  },
  activateButton: {
    width: "100%",
    backgroundColor: blue[500],
    "&:hover": {
      backgroundColor: blue[800],
    },
    color: "white",
  },
  activationSpinner: {
    color: grey[300],
  },
  activatedCheck: {
    color: grey[300],
    "-webkit-text-stroke": "2px #e0e0e0",
  },
}));

export default function UserActivateForm({ onSubmit, isSubmitting }) {
  const classes = useStyles();

  const [username, setUsername] = useState("");
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
              {"Activate Account"}
            </Box>
            <Box mt={1.5} fontWeight={300} fontSize={15} color={"black"}>
              <p>
                {
                  "To complete your registration please create a username and password."
                }
              </p>
            </Box>
            <Box mt={2}>
              <Box>
                <TextField
                  fullWidth
                  className={classes.textField}
                  id={"username"}
                  label={"Username"}
                  value={username}
                  variant={"outlined"}
                  onChange={(event) => setUsername(event.target.value)}
                />
              </Box>
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
                  className={classes.activateButton}
                  onClick={() => onSubmit(username, password)}
                >
                  Activate
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
