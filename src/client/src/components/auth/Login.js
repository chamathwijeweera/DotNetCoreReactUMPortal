import React, { Fragment, useState } from 'react';
import {Link, Redirect} from 'react-router-dom';
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {login} from '../../actions/auth';

const Login = ({login, isAuthenticated}) => {

    const [formData, setformData] = useState({
        email: '',
        password: ''
    });

    const { email, password } = formData;

    function onInputChange(e) {
        return setformData({ ...formData, [e.target.name]: e.target.value });
    }

    async function onFormSubmit(e) {
        e.preventDefault();
        login({email, password});
    }

    //Redirect if logged in
    if(isAuthenticated){
        return <Redirect to="/dashboard"/>
    }

    return (
        <Fragment>
            <h1 className="large text-primary">Sign In</h1>

            <form className="form" onSubmit={e => onFormSubmit(e)}>
                <div className="form-group">
                    <input type="text" placeholder="Email" name="email" value={email} onChange={e => onInputChange(e)} required />
                </div>
                <div className="form-group">
                    <input
                        type="password"
                        placeholder="Password"
                        name="password"
                        value={password}
                        onChange={e => onInputChange(e)}
                        required
                    />
                </div>
                <input type="submit" className="btn btn-primary" value="Login" />
            </form>
            <p className="my-1">
                Don't have an account? <Link to="/register">Sign up</Link>
            </p>

        </Fragment>
    )
}

login.PropTypes = {
    login: PropTypes.func.isRequired,
    isAuthenticated: PropTypes.bool
};

const mapStateToProps = state => ({
    isAuthenticated: state.auth.isAuthenticated
});


export default connect(mapStateToProps,{login})(Login);