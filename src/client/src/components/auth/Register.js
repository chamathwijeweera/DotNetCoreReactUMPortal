import React, { Fragment, useState } from 'react';
import { connect } from "react-redux";
import { Link, Redirect } from 'react-router-dom';
import { setAlert } from "../../actions/alert";
import { resgiter } from "../../actions/auth";
import PropTypes from 'prop-types'


const Register = ({ setAlert, resgiter, isAuthenticated }) => {

    const [formData, setformData] = useState({
        username: '',
        email: '',
        password: '',
        password2: '',
        userrole: ''
    });

    const { username, email, password, password2, userrole } = formData;

    function onInputChange(e) {
        return setformData({ ...formData, [e.target.name]: e.target.value });
    }

    async function onFormSubmit(e) {
        e.preventDefault();
        if (password !== password2) {
            setAlert('Passwords do not match', 'danger');
        }
        else {
            try {
                resgiter({ username, email, password, userrole });
            } catch (error) {
                console.log(error);
            }
        }
    }

    if(isAuthenticated)
    {
        return <Redirect to='/dashboard'/>
    }

    return (
        <Fragment>
            <h1 className="large text-primary">Create Account</h1>

            <form className="form" onSubmit={e => onFormSubmit(e)}>
                <div className="form-group">
                    <input type="text" placeholder="Name" name="username" value={username} onChange={e => onInputChange(e)} />
                </div>
                <div className="form-group">
                    <input type="email" placeholder="Email Address" name="email" value={email} onChange={e => onInputChange(e)} />
                </div>
                <div className="form-group">
                    <input
                        type="password"
                        placeholder="Password"
                        name="password"

                        value={password}
                        onChange={e => onInputChange(e)}

                    />
                </div>
                <div className="form-group">
                    <input
                        type="password"
                        placeholder="Confirm Password"
                        name="password2"

                        value={password2}
                        onChange={e => onInputChange(e)}

                    />
                </div>
                <div className="form-group">
                    <select name="userrole" value={userrole} onChange={e => onInputChange(e)}>
                        <option value="0">Select user role</option>
                        <option value="Administrator">Administrator</option>
                        <option value="Manager">Manager</option>
                        <option value="Developer">Developer</option>
                        <option value="Customer">Customer</option>
                    </select>
                </div>
                <input type="submit" className="btn btn-primary" value="Register" />
            </form>
            <p className="my-1">
                Don't have an account? <Link to="/register">Sign up</Link>
            </p>
        </Fragment>
    )
}
Register.protoTypes = {
    setAlert: PropTypes.func.isRequired,
    resgiter: PropTypes.func.isRequired,
    isAuthenticated : PropTypes.bool
}

const mapStateToProps = state => ({
    isAuthenticated: state.auth.isAuthenticated
});

export default connect(mapStateToProps, { setAlert, resgiter })(Register);

