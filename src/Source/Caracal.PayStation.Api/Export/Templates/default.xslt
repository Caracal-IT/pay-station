<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <xsl:for-each select="ArrayOfWithdrawal/Withdrawal">
            <xsl:element name="withdrawal">
                <xsl:attribute name="id">
                    <xsl:value-of select="Id"/>
                </xsl:attribute>
                <xsl:attribute name="account">
                    <xsl:value-of select="Account"/>
                </xsl:attribute>
                <xsl:attribute name="amount">
                    <xsl:value-of select="Amount"/>
                </xsl:attribute>
            </xsl:element>
        </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>