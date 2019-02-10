import yaml from 'js-yaml'
import fs from 'fs'
import * as R from 'ramda'
import changeCase from 'change-case'
import path from 'path'

const outputDir = '../RingCentral/Paths/Generated'

const doc = yaml.safeLoad(fs.readFileSync('rc-platform.yml', 'utf8'))
const paths = Object.keys(doc.paths)
const normalizedPaths = paths.map(p => p
  .replace(/\/restapi\/v1\.0/, '/restapi/{apiVersion}')
  .replace(/\/scim\/v2/, '/scim/{version}')
  .replace(/\/\.search/, '/dotSearch')
)

const firstLevels = R.pipe(
  R.map(p => p.split('/').filter(t => !R.isEmpty(t))[0]),
  R.uniq
)(paths)
console.log(firstLevels)

R.forEach(name => {
  const folderName = changeCase.pascalCase(name)
  const folderPath = path.join(outputDir, folderName)
  fs.mkdirSync(folderPath)
  var paramName = R.pipe(
    R.filter(p => p.startsWith(`/${name}/{`)),
    R.uniqBy(p => R.take(2, p.split('/').filter(t => t !== '')).join('/')),
    R.map(p => R.init(R.tail(p.split('/')[2]))),
    R.head
  )(normalizedPaths)
  console.log(paramName)
  let defaultParamValue
  if (name === 'restapi' && paramName === 'apiVersion') {
    defaultParamValue = 'v1.0'
  } else if (name === 'scim' && paramName === 'version') {
    defaultParamValue = 'v2'
  } else if (name === 'account' && paramName === 'accountId') {
    defaultParamValue = '~'
  } else if (name === 'extension' && paramName === 'extensionId') {
    defaultParamValue = '~'
  }

  let code = `namespace RingCentral.Paths.${folderName}
{
    public class Index
    {
      public RestClient rc;`

  if (paramName) {
    code += `
      public string ${paramName};`
  }

  if (paramName) {
    code += `
      public Index(RestClient rc, string ${paramName} = ${defaultParamValue ? `"${defaultParamValue}"` : null})
      {
          this.rc = rc;
          this.${paramName} = ${paramName};
      }`
  } else {
    code += `
      public Index(RestClient rc)
      {
          this.rc = rc;
      }`
  }

  code += `
    }
}`
  fs.writeFileSync(path.join(folderPath, 'Index.cs'), code)
})(firstLevels)
