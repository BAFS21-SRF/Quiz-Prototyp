module.exports = {
    runtimeCompiler: true,

    devServer: {
        proxy: 'http://localhost:8888',
    },

    transpileDependencies: [
      'vuetify'
    ]
}
