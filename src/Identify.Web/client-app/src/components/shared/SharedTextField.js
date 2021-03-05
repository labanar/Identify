import React from "react";
import { TextField, makeStyles } from "@material-ui/core";
import { grey } from "@material-ui/core/colors";

const useStyles = makeStyles((theme) => ({
  cssLabel: {
    color: grey[200],
  },
  cssOutlinedInput: {
    color: "#bfbfbf", // <!-- ADD THIS ONE
    "&$cssFocused $notchedOutline": {
      borderColor: "#bfbfbf !important",
    },
  },
  cssFocused: { color: "#bfbfbf !important" },
  notchedOutline: {
    borderWidth: "1px",
    borderColor: "#bfbfbf !important",
  },
}));

export default function SharedTextField({
  id,
  label,
  value,
  setValue,
  password,
  className,
}) {
  const tfClasses = useStyles();

  return (
    <TextField
      id={id}
      label={label}
      variant="outlined"
      value={value}
      className={className}
      type={password ? "password" : ""}
      onChange={(e) => setValue(e.target.value)}
      InputLabelProps={{
        classes: {
          root: tfClasses.cssLabel,
          focused: tfClasses.cssFocused,
        },
      }}
      InputProps={{
        classes: {
          root: tfClasses.cssOutlinedInput,
          focused: tfClasses.cssFocused,
          notchedOutline: tfClasses.notchedOutline,
        },
      }}
    />
  );
}
