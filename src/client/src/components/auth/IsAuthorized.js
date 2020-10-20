import PropTypes from "prop-types";
import rules from "../../permissions";
import { connect } from "react-redux";
import _ from 'lodash';

const checkUserPermission = (auth, module, allowedActions) => {

  var authorized = false;

  if (!auth.loading) {

    var user = auth.user;

    _.forEach(user.roles, function (role) {

      const permissions = rules[role];

      if (permissions) {

        const staticRules = permissions.static;

        if (staticRules) {

          _.forEach(staticRules, function (permission) {

            if (permission.moduleId === module && permission.operationId == allowedActions) {
              authorized = true;
            }

            // _.forEach(staticRules, function (rule) {

            //   if (permission.moduleId === rule.moduleId && permission.operationId == rule.operationId) {
            //     authorized = true;
            //   }
            // })
          })
        }
      }
    })
  }

  return authorized;
};

const IsAuthorized = ({ auth, module, perform, yes, no }) => (

  checkUserPermission(auth, module, perform) ? yes() : no()

);





const mapStateToProps = state => ({
  auth: state.auth
});

export default connect(mapStateToProps)(IsAuthorized);