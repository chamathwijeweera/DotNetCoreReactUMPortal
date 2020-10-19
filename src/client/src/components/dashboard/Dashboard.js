import React from 'react'
import PropTypes from 'prop-types'
import { connect } from "react-redux";
import IsAuthorized from "../auth/IsAuthorized";

const Dashboard = ({auth: { loading, user }}) => {
    return (
        <IsAuthorized
        roles={user.userRoles}
        permissions={user.userPermissions}
        perform="1"
        yes={() => (
          <div>
            <h1>You have create permission</h1>
          </div>
        )}
        no={() => (
            <div>
                <h1>You don't have create permission</h1>
            </div>
        )}
        />
        // <div>
        //     <h1>Lol</h1>
        // </div>
    )
}

Dashboard.propTypes = {

}

const mapStateToProps = state => ({
    auth: state.auth
});

export default connect(mapStateToProps)(Dashboard)
