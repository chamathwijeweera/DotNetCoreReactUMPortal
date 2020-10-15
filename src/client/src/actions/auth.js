import axios from "axios";
import { REGISTER_SUCCESS, REGISTER_FAIL, USER_LOADED, AUTH_ERROR, LOGIN_SUCCESS, LOGIN_FAIL, LOGOUT } from "./types";
import { setAlert } from "./alert";
import setAuthToken from "../utils/setAuthToken";

const urlBase = 'https://localhost:44361';

//Load User
export const loadUser = () => async dispatch => {

    if (localStorage.atk) {
        setAuthToken(localStorage.atk)
    }

    try {
        const response = await axios.get(`${urlBase}/api/accounts/authuser`);

        dispatch({
            type: USER_LOADED,
            payload: response.data
        });
    } catch (error) {
        dispatch({
            type: AUTH_ERROR
        });
    }
}

//Register User
export const resgiter = (userData) => async dispatch => {
    const config = {
        headers: {
            'Content-Type': 'application/json'
        }
    }

    const body = JSON.stringify(userData);

    try {
        const response = await axios.post(`${urlBase}/api/accounts/register`, body, config);
        dispatch({
            type: REGISTER_SUCCESS,
            payload: response.data
        });
    } catch (error) {

        const errorObj = error.response.data;

        if (errorObj.status === "Error") {

            if (errorObj.errors === null) {
                dispatch(setAlert(errorObj.message, 'danger'));
            }
            else {
                errorObj.errors.forEach(error => dispatch(setAlert(error.description, 'danger')));
            }
        }
        else {
            for (let key in errorObj.errors) {
                errorObj[key].forEach(error => dispatch(setAlert(error, 'danger')));
            }
        }

        dispatch({
            type: REGISTER_FAIL
        });
    }
};

//Login User
export const login = (userCredentials) => async dispatch => {
    const config = {
        headers: {
            'Content-Type': 'application/json'
        }
    }

    const body = JSON.stringify(userCredentials);

    try {
        const response = await axios.post(`${urlBase}/api/accounts/login`, body, config);
        dispatch({
            type: LOGIN_SUCCESS,
            payload: response.data
        });
    } catch (error) {

        const errorObj = error.response.data;

        if (errorObj.status === 401) {

            dispatch(setAlert("Login failed", 'danger'));
        }
        else {
            for (let key in errorObj.errors) {
                errorObj[key].forEach(error => dispatch(setAlert(error, 'danger')));
            }
        }

        dispatch({
            type: LOGIN_FAIL
        });
    }
};

//Logout //clear Profile / ToDo- change 
export const logout = () => async dispatch => {
    dispatch({
        type: LOGOUT
    });
}