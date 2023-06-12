import React from "react";

export default function useUserModel() {
    const [name, setName] = React.useState("");

    return{
        name, 
        setName   
    }
}