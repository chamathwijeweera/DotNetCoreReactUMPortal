import React, { Fragment } from 'react'
import PropTypes from 'prop-types'
import { connect } from "react-redux";
import IsAuthorized from "../auth/IsAuthorized";
import _ from 'lodash';

const Dashboard = ({ auth: { loading, user } }) => {
    return (
        <div>
            <h1>Welcome - {!loading && user.name}</h1><br />
 
               { !loading && <h4>Roles - {_.join(user.roles, ' | ')}</h4>}
            
            <br />
            {
                !loading &&
                <IsAuthorized module="12" perform="1"
                    yes={() => (
                        <div>
                            <input type="submit" className="btn btn-primary" value="Creat" />
                        </div>
                    )}
                    no={() => (
                        <Fragment />
                    )}
                />
            }
            {
                !loading &&
                <IsAuthorized module="12" perform="3"
                    yes={() => (
                        <div>
                            <input type="submit" className="btn btn-primary" value="Update" />
                        </div>
                    )}
                    no={() => (
                        <Fragment />
                    )}
                />
            }
            {
                !loading &&
                <IsAuthorized module="14" perform="2"
                    yes={() => (
                        <div>
                            <input type="submit" className="btn btn-primary" value="View" />
                        </div>
                    )}
                    no={() => (
                        <Fragment />
                    )}
                />
            }
            {
                !loading &&
                <IsAuthorized module="13" perform="5"
                    yes={() => (
                        <div>
                            <input type="submit" className="btn btn-primary" value="Execute" />
                        </div>
                    )}
                    no={() => (
                        <Fragment />
                    )}
                />
            }
        </div>
    )
}

Dashboard.propTypes = {

}

const mapStateToProps = state => ({
    auth: state.auth
});

export default connect(mapStateToProps)(Dashboard)
