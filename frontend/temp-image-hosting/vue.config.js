// module.exports = {
//   configureWebpack: config => {
//     if (process.env.NODE_ENV === 'production') {
//       return null;
//     } else {
//       return {
//         resolve: {
//           alias: {
//             'vue$': 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' for webpack 1
//           }
//         }
//       }
//     }
//   }
// }

module.exports = {
  configureWebpack: {
    resolve: {
      alias: {
        'vue$': 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' for webpack 1
      }
    }
  }
}