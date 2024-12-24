module.exports = {
    testEnvironment: 'jest-environment-jsdom',
    transform: {
      '^.+\\.jsx?$': 'babel-jest'
    },
    moduleFileExtensions: ['js', 'jsx'],
    roots: ['<rootDir>/wwwroot/js/'],
  };
  