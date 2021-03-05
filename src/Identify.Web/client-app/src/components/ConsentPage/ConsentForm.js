import { Box, makeStyles, Paper } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import React from "react";
import { useDispatch } from "react-redux";
import { useHistory, useLocation } from "react-router";

const useStyles = makeStyles((theme) => ({}));

export default function ConsentForm({ onSubmit, isSubmitting }) {
  const classes = useStyles();
  const dispatch = useDispatch();
  const history = useHistory();
  const location = useLocation();

  //   const authUrl = queryParams["returnUrl"].split("?")[0];

  const renderFormLoading = () => {
    return (
      <Box
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"center"}
        p={3}
      >
        <Box mt={1} fontWeight={400} fontSize={20} color={"black"}>
          <Skeleton variant="rect" width={"33%"} height={30} />
        </Box>
        <Box mt={1.5} fontWeight={400} fontSize={15} color={"black"}>
          <Skeleton variant="rect" width={"45%"} height={18} />
        </Box>
        <Box mt={3} fontWeight={400} fontSize={20} color={"black"}>
          <Skeleton variant="rect" width={"55%"} height={26} />
        </Box>
        <Box mt={3} fontWeight={400} fontSize={15} color={"black"}>
          <Skeleton variant="rect" width={"75%"} height={23} />
        </Box>
      </Box>
    );
  };

  return (
    <Box
      display={"flex"}
      flexDirection={"column"}
      justifyContent={"center"}
      minWidth={"360px"}
      maxWidth={"360px"}
    >
      <Paper>{renderFormLoading()}</Paper>
    </Box>
  );
}

function ApiScopeDetail({ loading, clientName, displayName, description }) {
  return (
    <>
      <Box ml={1.5} mt={1.5} fontWeight={400} fontSize={15} color={"black"}>
        {loading && <Skeleton variant="rect" width={"35%"} height={23} />}
        {!loading && displayName}
        <Box fontWeight={300} fontSize={14} color={"black"} mt={0.5}>
          {loading && <Skeleton variant="rect" width={"100%"} height={45} />}
          {!loading && `${clientName} will be able to ${description}.`}
        </Box>
      </Box>
    </>
  );
}
