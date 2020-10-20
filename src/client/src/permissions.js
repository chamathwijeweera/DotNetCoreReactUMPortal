const rules = {
    administrator: {
      static: [
         {moduleId: "11", operationId: "1"},
         {moduleId: "11", operationId: "2"},
         {moduleId: "11", operationId: "3"},
         {moduleId: "11", operationId: "4"},
         {moduleId: "11", operationId: "5"}
      ]
    },
    manager: {
      static: [
         {moduleId: "12", operationId: "1"},
         {moduleId: "12", operationId: "2"},
         {moduleId: "12", operationId: "3"}
      ]
    },
    developer: {
      static: [
         {moduleId: "13", operationId: "5"}
      ]
    },
    customer: {
      static: [
         {moduleId: "14", operationId: "2"},
      ]
    }
  };
  
  export default rules;


  // Create = 1,
  //       Read = 2,
  //       Update = 3,
  //       Delete = 4,
  //       Execute = 5

  //       Administrator = 11,
  //       Manager = 12,
  //       Developer = 13,
  //       Customer = 14,