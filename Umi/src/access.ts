import React from "react";

export default function (initialState) {
    //const { userId, role } = initialState;

    const token = localStorage.getItem('token');

   
    return {
      isUser: (token ? true : false) 

      };
    }