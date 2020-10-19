import PropTypes from "prop-types";
import rules from "../../permissions";

const check = (roles, per, data) => {
  const permissions = rules[role];
  if (!permissions) {
    // role is not present in the rules
    return false;
  }

  const staticPermissions = permissions.static;

  if (staticPermissions && staticPermissions.includes(action)) {
    // static rule not provided for action
    return true;
  }

  return false;
};

const IsAuthorized = (props) =>
  check(props.roles, props.permissions, props.perform,) ? props.yes() : props.no();

IsAuthorized.propTypes  = {
  yes: () => null,
  no: () => null
};

export default IsAuthorized;