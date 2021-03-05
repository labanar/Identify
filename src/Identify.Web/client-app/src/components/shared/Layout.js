import { makeStyles } from "@material-ui/core";
import React from "react";

const useStyles = makeStyles((theme) => ({
  root: {
    background: "linear-gradient(to right bottom, #141e30, #345478)",
  },
}));

export default function Layout({ children }) {
  const classes = useStyles();
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
      {children}
    </div>
  );
}
