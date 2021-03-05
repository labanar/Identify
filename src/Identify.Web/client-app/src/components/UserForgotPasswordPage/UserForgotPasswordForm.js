import {
  Box,
  Button,
  makeStyles,
  Paper,
  TextField,
  Typography,
} from "@material-ui/core";
import { blue, grey } from "@material-ui/core/colors";
import React, { useState } from "react";

const useStyles = makeStyles((theme) => ({
  textField: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
    color: "black",
  },
  acceptButton: {
    width: "100%",
    backgroundColor: blue[500],
    "&:hover": {
      backgroundColor: blue[800],
    },
    color: "white",
  },
}));

export function UserForgotPasswordForm({ onSubmit, isSubmitting }) {
  const classes = useStyles();
  const [email, setEmail] = useState("");
  const [hasSubmitted, setHasSubmitted] = useState(false);

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
              {"Forgot your password?"}
            </Box>
            <Box mt={1.5} fontWeight={300} fontSize={15} color={"black"}>
              <p style={{ maxWidth: "400px" }}>
                {
                  "To reset your password, enter the email address associated with your account. You will be sent an email containing password reset link."
                }
              </p>
            </Box>
            <Box mt={2}>
              <Box>
                <TextField
                  fullWidth
                  className={classes.textField}
                  id={"email"}
                  label={"Email"}
                  value={email}
                  variant={"outlined"}
                  onChange={(event) => setEmail(event.target.value)}
                />
              </Box>
            </Box>
            <Box mt={3} color={"black"} display={"flex"}>
              <Box width={"100%"} mr={0.5}>
                <Button
                  variant={"text"}
                  className={classes.acceptButton}
                  onClick={() => {
                    onSubmit(email);
                    setHasSubmitted(true);
                  }}
                >
                  Accept
                </Button>
              </Box>
            </Box>
          </Box>
        </Paper>
      </Box>
    );
  };

  const renderSubmitted = (classes) => {
    return (
      <Typography color={"textSecondary"}>
        Check your inbox for a password reset link
      </Typography>
    );
  };

  return (
    <>
      {!hasSubmitted && renderForm(classes)}
      {hasSubmitted && renderSubmitted(classes)}
    </>
  );
}
